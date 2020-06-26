module Index

open Giraffe.GiraffeViewEngine


let links =
    div [ _class "flex flex-row" ]
        [ a
            [ _class "mr-2 pt-2 pb-2 pl-4 pr-3 rounded-sm bg-blue-600 text-white"
              _href "/otherView" ] [ str "Other View" ]
          a
              [ _class "mr-2 pt-2 pb-2 pl-4 pr-3 rounded-sm bg-blue-600 text-white"
                _href "/dynamicView" ] [ str "Dynamic View" ] ]

let index = [ Helpers.contentBox links ]

let layout = App.layout index
