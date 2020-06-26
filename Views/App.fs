module App

open Feliz.ViewEngine

let layout content =
    Html.html
        [ Html.head
            [ Html.meta
                [ prop.charset "utf-8"
                  prop.name "viewport"
                  prop.content "width=device-width, initial-scale=1" ]
              Html.title "Hello Saturn + Turbolinks + htmx"
              Html.link
                  [ prop.rel "stylesheet"
                    prop.href "https://unpkg.com/tailwindcss@^1.0/dist/tailwind.min.css" ]
              Html.script [ prop.src "https://cdnjs.cloudflare.com/ajax/libs/turbolinks/5.1.1/turbolinks.js" ] ]
          Html.body
              [ Html.div
                  [ prop.className "mx-auto bg-gray-300 text-gray-700 min-h-screen flex flex-col"
                    prop.children
                        [ Html.header
                            [ prop.className "bg-purple-900 text-white mb-4"
                              prop.children
                                  [ Html.div
                                      [ prop.className "flex flex-row justify-between pt-4 pl-2 mb-4 mx-3"
                                        prop.children
                                            [ Html.h1
                                                [ prop.className "text-2xl font-semibold"
                                                  prop.text "Saturn + Turbolinks + htmx" ] ] ] ] ]
                          Helpers.hero
                          Html.div
                              [ prop.className "container mx-auto"
                                prop.children content ] ] ]
                Html.script [ prop.src "https://unpkg.com/htmx.org@0.0.6" ] ] ]
