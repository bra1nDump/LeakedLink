namespace LeakedLink

open FSharp.Data
open HttpFs.Client
open Request
open Response
open Hopac
open Infixes

module SpotifyApi =

    type TrackResponseTypeProvider = 
        JsonProvider<"""
        {
            "album": {
                "album_type": "album",
                "artists": [
                    {
                        "external_urls": {
                            "spotify": "https://open.spotify.com/artist/6U3ybJ9UHNKEdsH7ktGBZ7"
                        },
                        "href": "https://api.spotify.com/v1/artists/6U3ybJ9UHNKEdsH7ktGBZ7",
                        "id": "6U3ybJ9UHNKEdsH7ktGBZ7",
                        "name": "JID",
                        "type": "artist",
                        "uri": "spotify:artist:6U3ybJ9UHNKEdsH7ktGBZ7"
                    }
                ],
                "available_markets": [
                    "AD"
                ],
                "external_urls": {
                    "spotify": "https://open.spotify.com/album/1gPqbxhs90kppgOVxGOPzd"
                },
                "href": "https://api.spotify.com/v1/albums/1gPqbxhs90kppgOVxGOPzd",
                "id": "1gPqbxhs90kppgOVxGOPzd",
                "images": [
                    {
                        "height": 640,
                        "url": "https://i.scdn.co/image/afdccc573b3923e0d7a56dfcd7ef1c8eb052fedf",
                        "width": 640
                    }
                ],
                "name": "The Never Story",
                "release_date": "2017-03-10",
                "release_date_precision": "day",
                "total_tracks": 12,
                "type": "album",
                "uri": "spotify:album:1gPqbxhs90kppgOVxGOPzd"
            },
            "artists": [
                {
                    "external_urls": {
                        "spotify": "https://open.spotify.com/artist/6U3ybJ9UHNKEdsH7ktGBZ7"
                    },
                    "href": "https://api.spotify.com/v1/artists/6U3ybJ9UHNKEdsH7ktGBZ7",
                    "id": "6U3ybJ9UHNKEdsH7ktGBZ7",
                    "name": "JID",
                    "type": "artist",
                    "uri": "spotify:artist:6U3ybJ9UHNKEdsH7ktGBZ7"
                }
            ],
            "available_markets": [
                "AD"
            ],
            "disc_number": 1,
            "duration_ms": 241682,
            "explicit": true,
            "external_ids": {
                "isrc": "USUM71615106"
            },
            "external_urls": {
                "spotify": "https://open.spotify.com/track/7hsulgRNgbyczeAg8tChCB"
            },
            "href": "https://api.spotify.com/v1/tracks/7hsulgRNgbyczeAg8tChCB",
            "id": "7hsulgRNgbyczeAg8tChCB",
            "is_local": false,
            "name": "NEVER",
            "popularity": 68,
            "preview_url": null,
            "track_number": 3,
            "type": "track",
            "uri": "spotify:track:7hsulgRNgbyczeAg8tChCB"
        }
        """>

    let getTrack id =
        job {
            try
                return!
                    createUrl Get ("https://api.spotify.com/v1/tracks/" + id)
                    |> Request.setHeader (RequestHeader.Authorization ("Bearer " + Secrets.spotifyToken))
                    |> getResponse
                    >>= readBodyAsString
                    >>- TrackResponseTypeProvider.Parse
                    >>- Ok
            with
            | exn -> return Error (exn.ToString())
        }
        |> Job.toAsync