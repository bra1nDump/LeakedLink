namespace LeakedLink

open FSharp.Data
open HttpFs.Client
open Request
open Response
open Hopac
open Infixes

module AppleMusicApi =

    type CatalogSearchResponseTypeProvider = 
        JsonProvider<"""
        {
            "results": {
                "songs": {
                    "href": "/v1/catalog/us/search?term=JID+NEVER&types=songs",
                    "next": "/v1/catalog/us/search?offset=5&term=JID+NEVER&types=songs",
                    "data": [
                        {
                            "id": "1211536000",
                            "type": "songs",
                            "href": "/v1/catalog/us/songs/1211536000",
                            "attributes": {
                                "previews": [
                                    {
                                        "url": "https://audio-ssl.itunes.apple.com/apple-assets-us-std-000001/AudioPreview111/v4/39/66/a3/3966a38f-a99b-f097-0682-869ba0903fd8/mzaf_2919135559670108428.plus.aac.p.m4a"
                                    }
                                ],
                                "artwork": {
                                    "width": 1800,
                                    "height": 1800,
                                    "url": "https://is4-ssl.mzstatic.com/image/thumb/Music122/v4/58/f4/7d/58f47d6c-48df-5009-0e01-fa191c0f7fe4/UMG_cvrart_00602557393255_01_RGB72_1800x1800_16UMGIM88339.jpg/{w}x{h}bb.jpeg",
                                    "bgColor": "221c1c",
                                    "textColor1": "fbb926",
                                    "textColor2": "fc9c0c",
                                    "textColor3": "cf9924",
                                    "textColor4": "d0820f"
                                },
                                "artistName": "JID",
                                "url": "https://itunes.apple.com/us/album/never/1211535987?i=1211536000",
                                "discNumber": 1,
                                "genreNames": [
                                    "Hip-Hop/Rap",
                                    "Music"
                                ],
                                "durationInMillis": 241682,
                                "releaseDate": "2016-12-16",
                                "name": "NEVER",
                                "isrc": "USUM71615106",
                                "albumName": "The Never Story",
                                "playParams": {
                                    "id": "1211536000",
                                    "kind": "song"
                                },
                                "trackNumber": 3,
                                "composerName": "John Welch, Marcus Alandrus Randle & Destin Route",
                                "contentRating": "explicit"
                            }
                        }
                    ]
                },
                "albums": {
                    "href": "/v1/catalog/us/search?term=JID+NEVER&types=albums",
                    "next": "/v1/catalog/us/search?offset=5&term=JID+NEVER&types=albums",
                    "data": [
                        {
                            "id": "1211535987",
                            "type": "albums",
                            "href": "/v1/catalog/us/albums/1211535987",
                            "attributes": {
                                "artwork": {
                                    "width": 1800,
                                    "height": 1800,
                                    "url": "https://is4-ssl.mzstatic.com/image/thumb/Music122/v4/58/f4/7d/58f47d6c-48df-5009-0e01-fa191c0f7fe4/UMG_cvrart_00602557393255_01_RGB72_1800x1800_16UMGIM88339.jpg/{w}x{h}bb.jpeg",
                                    "bgColor": "221c1c",
                                    "textColor1": "fbb926",
                                    "textColor2": "fc9c0c",
                                    "textColor3": "cf9924",
                                    "textColor4": "d0820f"
                                },
                                "artistName": "JID",
                                "isSingle": false,
                                "url": "https://itunes.apple.com/us/album/the-never-story/1211535987",
                                "isComplete": true,
                                "genreNames": [
                                    "Hip-Hop/Rap",
                                    "Music",
                                    "Underground Rap",
                                    "Rap"
                                ],
                                "trackCount": 12,
                                "isMasteredForItunes": false,
                                "releaseDate": "2017-03-10",
                                "name": "The Never Story",
                                "recordLabel": "J. Cole/ DreamVille",
                                "copyright": "℗ 2017 Dreamville/Interscope",
                                "playParams": {
                                    "id": "1211535987",
                                    "kind": "album"
                                },
                                "editorialNotes": {
                                    "short": "Loose, agile flows and an assist from J. Cole—a stunning arrival."
                                },
                                "contentRating": "explicit"
                            }
                        }
                    ]
                },
                "music-videos": {
                    "href": "/v1/catalog/us/search?term=JID+NEVER&types=music-videos",
                    "next": "/v1/catalog/us/search?offset=5&term=JID+NEVER&types=music-videos",
                    "data": [
                        {
                            "id": "1458124590",
                            "type": "music-videos",
                            "href": "/v1/catalog/us/music-videos/1458124590",
                            "attributes": {
                                "previews": [
                                    {
                                        "url": "https://video-ssl.itunes.apple.com/itunes-assets/Video123/v4/d6/82/50/d682503a-73d9-74b0-26e5-c1086fd712d8/mzvf_589826117496759496.720w.h264lc.U.p.m4v",
                                        "hlsUrl": "https://play.itunes.apple.com/WebObjects/MZPlay.woa/hls/playlist.m3u8?cc=US&a=1458124590&id=59388223&l=en&aec=HD",
                                        "artwork": {
                                            "width": 1912,
                                            "height": 811,
                                            "url": "https://is2-ssl.mzstatic.com/image/thumb/Video113/v4/df/ff/19/dfff19be-3e8b-a285-871b-665bf0a813c0/00602577645884_00001.crop.jpg/{w}x{h}bb.jpeg",
                                            "bgColor": "03134c",
                                            "textColor1": "e5bc90",
                                            "textColor2": "f76086",
                                            "textColor3": "b89b83",
                                            "textColor4": "c6517b"
                                        }
                                    }
                                ],
                                "artwork": {
                                    "width": 1912,
                                    "height": 811,
                                    "url": "https://is2-ssl.mzstatic.com/image/thumb/Video113/v4/df/ff/19/dfff19be-3e8b-a285-871b-665bf0a813c0/00602577645884_00001.crop.jpg/{w}x{h}bb.jpeg",
                                    "bgColor": "03134c",
                                    "textColor1": "e5bc90",
                                    "textColor2": "f76086",
                                    "textColor3": "b89b83",
                                    "textColor4": "c6517b"
                                },
                                "artistName": "Rich The Kid",
                                "url": "https://itunes.apple.com/us/music-video/for-keeps-feat-youngboy-never-broke-again/1458124590",
                                "genreNames": [
                                    "Hip-Hop/Rap"
                                ],
                                "has4K": false,
                                "durationInMillis": 172180,
                                "releaseDate": "2019-04-04",
                                "name": "For Keeps (feat. YoungBoy Never Broke Again)",
                                "isrc": "USUV71901008",
                                "playParams": {
                                    "id": "1458124590",
                                    "kind": "musicVideo"
                                },
                                "hasHDR": false,
                                "contentRating": "explicit"
                            }
                        }
                    ]
                },
                "artists": {
                    "href": "/v1/catalog/us/search?term=JID+NEVER&types=artists",
                    "data": [
                        {
                            "id": "378744030",
                            "type": "artists",
                            "href": "/v1/catalog/us/artists/378744030",
                            "attributes": {
                                "url": "https://itunes.apple.com/us/artist/kids-never-lie/378744030",
                                "genreNames": [
                                    "Alternative"
                                ],
                                "name": "Kids Never Lie"
                            },
                            "relationships": {
                                "albums": {
                                    "data": [
                                        {
                                            "id": "378744026",
                                            "type": "albums",
                                            "href": "/v1/catalog/us/albums/378744026"
                                        }
                                    ],
                                    "href": "/v1/catalog/us/artists/378744030/albums"
                                }
                            }
                        }
                    ]
                }
            }
        }
        """>
    
    let catalogSearch artist songName =
        let term = String.replace " " "+" (artist + "+" + songName)
        job {
            try
                return!
                    createUrl Get ("https://api.music.apple.com/v1/catalog/us/search?term=" + term)
                    |> Request.setHeader (RequestHeader.Authorization ("Bearer " + Secrets.appleMusicToken))
                    |> getResponse
                    >>= readBodyAsString
                    >>- CatalogSearchResponseTypeProvider.Parse
                    >>- Ok
            with
            | exn -> return Error (exn.ToString())
        }
        |> Job.toAsync