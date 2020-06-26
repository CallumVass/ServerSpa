module OtherView

open Feliz.ViewEngine

let link =
    Html.a
        [ prop.className "pt-2 pb-2 pl-4 pr-3 rounded-sm bg-blue-600 text-white"
          prop.href "/"
          prop.text "Back to Main" ]

let index = Helpers.contentBox link

let layout = (App.layout index)
