module App

open Giraffe.GiraffeViewEngine

let layout (content: XmlNode list) =
    html [ _class "has-navbar-fixed-top" ]
        [ head []
              [ meta [ _charset "utf-8" ]
                meta
                    [ _name "viewport"
                      _content "width=device-width, initial-scale=1" ]
                title [] [ encodedText "Hello Saturn + Turbolinks + htmx" ]
                link
                    [ _rel "stylesheet"
                      _href "https://unpkg.com/tailwindcss@^1.0/dist/tailwind.min.css" ]
                script [ _src "https://cdnjs.cloudflare.com/ajax/libs/turbolinks/5.1.1/turbolinks.js" ] [] ]
          body []
              [ yield div [ _class "mx-auto bg-gray-300 text-gray-700" ]
                          [ div [ _class "min-h-screen flex flex-col" ]
                                [ yield header [ _class "bg-purple-900 text-white mb-4" ]
                                            [ div [ _class "flex flex-row justify-between pt-4 pl-2 mb-4 mx-3" ]
                                                  [ div []
                                                        [ h1 [ _class "text-2xl font-semibold" ]
                                                              [ str "Saturn + Turbolinks + htmlx" ] ] ] ]
                                  yield Helpers.hero
                                  yield div [ _class "container mx-auto" ] content ] ]
                yield script [ _src "https://unpkg.com/htmx.org@0.0.6" ] [] ] ]
