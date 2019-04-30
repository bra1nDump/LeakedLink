// Copyright 2018 Fabulous contributors. See LICENSE.md for license.
namespace LeakedLink

open System.Diagnostics
open Fabulous.Core
open Fabulous.DynamicViews
open Xamarin.Forms

module App = 
    type Model = {
            SpotifyLink: string
            AppleMusicLink: string
        }

    type Msg = 
        | SpotifyLinkTextChanged of string
        | GetAppleMusicLinkButtonPressed
        | GetAppleMusicLinkResult of Result<string, string>

    let init () =
        {
            SpotifyLink = "https://open.spotify.com/track/7hsulgRNgbyczeAg8tChCB?si=UaIhT7VYSi-AJKNM_k0eow"
            AppleMusicLink = ""
        }
        , Cmd.none

    let translateSpotifyToAppleMusic (link: string) =
        let spotifyTrackId = link.Substring(31, 22)
        async {
            try
                match! SpotifyApi.getTrack spotifyTrackId with
                | Error err -> return Error err
                | Ok track ->
                    let artistName = 
                        Array.get track.Artists 0 
                        |> fun artist -> artist.Name
                    match! AppleMusicApi.catalogSearch artistName track.Name with
                    | Error err -> return Error err
                    | Ok appleSearchResult ->
                        return
                            Array.get appleSearchResult.Results.Songs.Data 0
                            |> fun song -> song.Attributes.Url
                            |> Ok
            with
            | exn -> return Error (exn.ToString())
        }

    let update msg model =
        match msg with 
        | SpotifyLinkTextChanged link -> 
            { model with SpotifyLink = link }, Cmd.none
        | GetAppleMusicLinkButtonPressed ->
            model
            , translateSpotifyToAppleMusic model.SpotifyLink
            |> Async.map GetAppleMusicLinkResult
            |> Cmd.ofAsyncMsg
        | GetAppleMusicLinkResult (Ok link) ->
            { model with AppleMusicLink = link }, Cmd.none
        | _ -> model, Cmd.none

    let view (model: Model) dispatch =
        View.ContentPage(
            content = View.StackLayout(padding = 20.0, verticalOptions = LayoutOptions.Center
            , children = 
                [
                    View.Entry(
                        text = model.SpotifyLink
                        , placeholder = "Enter spotify link"
                        , textChanged = (fun args -> SpotifyLinkTextChanged args.NewTextValue |> dispatch))
                    View.Button(text = "Get Apple Music link", command = (fun _ -> GetAppleMusicLinkButtonPressed |> dispatch))
                    View.Entry(text = model.AppleMusicLink, isEnabled = false)
                ]))

    // Note, this declaration is needed if you enable LiveUpdate
    let program = Program.mkProgram init update view

type App () as app = 
    inherit Application ()

    let runner = 
        App.program
#if DEBUG
        |> Program.withConsoleTrace
#endif
        |> Program.runWithDynamicView app

#if DEBUG
    // Uncomment this line to enable live update in debug mode. 
    // See https://fsprojects.github.io/Fabulous/tools.html for further  instructions.
    //
    //do runner.EnableLiveUpdate()
#endif    

    // Uncomment this code to save the application state to app.Properties using Newtonsoft.Json
    // See https://fsprojects.github.io/Fabulous/models.html for further  instructions.
#if APPSAVE
    let modelId = "model"
    override __.OnSleep() = 

        let json = Newtonsoft.Json.JsonConvert.SerializeObject(runner.CurrentModel)
        Console.WriteLine("OnSleep: saving model into app.Properties, json = {0}", json)

        app.Properties.[modelId] <- json

    override __.OnResume() = 
        Console.WriteLine "OnResume: checking for model in app.Properties"
        try 
            match app.Properties.TryGetValue modelId with
            | true, (:? string as json) -> 

                Console.WriteLine("OnResume: restoring model from app.Properties, json = {0}", json)
                let model = Newtonsoft.Json.JsonConvert.DeserializeObject<App.Model>(json)

                Console.WriteLine("OnResume: restoring model from app.Properties, model = {0}", (sprintf "%0A" model))
                runner.SetCurrentModel (model, Cmd.none)

            | _ -> ()
        with ex -> 
            App.program.onError("Error while restoring model found in app.Properties", ex)

    override this.OnStart() = 
        Console.WriteLine "OnStart: using same logic as OnResume()"
        this.OnResume()
#endif


