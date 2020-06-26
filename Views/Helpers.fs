module Helpers

open Giraffe.GiraffeViewEngine

let hero =
    div [ _class "container mx-auto py-6 md:py-12 bg-white px-6 mb-4 rounded-sm text-center" ]
        [ h1 [ _class "font-bold text-3xl" ] [ str "Saturn + Turbolinks + htmx" ] ]

let contentBox content =
    div [ _class "mb-2 p-4 bg-white rounded-sm" ] [ content ]
