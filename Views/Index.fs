module Index

open Feliz.ViewEngine


let links =
    Html.div
        [ prop.className "flex flex-row"
          prop.children
              [ Html.a
                  [ prop.className "mr-2 pt-2 pb-2 pl-4 pr-3 rounded-sm bg-blue-600 text-white"
                    prop.href "/otherView"
                    prop.text "Other View" ]
                Html.a
                    [ prop.className "mr-2 pt-2 pb-2 pl-4 pr-3 rounded-sm bg-blue-600 text-white"
                      prop.href "/dynamicView"
                      prop.text "Dynamic View" ] ] ]

let index = Helpers.contentBox links

let layout = (App.layout index)
