module Helpers

open Feliz.ViewEngine

let hero =
    Html.div
        [ prop.className "container mx-auto py-6 md:py-12 bg-white px-6 mb-4 rounded-sm text-center"
          prop.children
              [ Html.h1
                  [ prop.className "font-bold text-3xl"
                    prop.text "Saturn + Turbolinks + htmx" ] ] ]

let contentBox content =
    Html.div
        [ prop.className "mb-2 p-4 bg-white rounded-sm"
          prop.children [ content ] ]
