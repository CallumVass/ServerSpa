module OtherView

open Giraffe.GiraffeViewEngine

let index =
    [ Helpers.contentBox
        (a
            [ _class "pt-2 pb-2 pl-4 pr-3 rounded-sm bg-blue-600 text-white"
              _href "/" ] [ str "Back to Main" ]) ]

let layout = App.layout index
