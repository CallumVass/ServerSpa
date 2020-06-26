module DynamicView

open Feliz.ViewEngine

let timeLayout time =
    Html.div (sprintf "Current Time is: %s" time)

let private fromClient time =
    Html.div [ prop.children [ Html.p "Client"; timeLayout time ] ]

let private fromServer time =
    Html.div
        [ prop.children
            [ Html.p "Server"
              Html.div
                  [ prop.custom ("hx-get", "/time")
                    prop.custom ("hx-trigger", "every 1s")
                    prop.children [ timeLayout time ] ] ] ]

let private form =
    Html.form
        [ prop.className "w-full max-w-sm"
          prop.custom ("hx-get", "/name")
          prop.custom ("hx-target", "#greeting")
          prop.children
              [ Html.div
                  [ prop.className "flex items-center border-b border-b-2 border-purple-500 py-2"
                    prop.children
                        [ Html.input
                            [ prop.name "name"
                              prop.placeholder "Enter your name"
                              prop.className
                                  "appearance-none bg-transparent border-none w-full text-gray-700 mr-3 py-1 px-2 leading-tight focus:outline-none" ]
                          Html.button
                              [ prop.className
                                  "flex-shrink-0 bg-purple-500 hover:bg-purple-700 border-purple-500 hover:border-purple-700 text-sm border-4 text-white py-1 px-2 rounded"
                                prop.type' "submit"
                                prop.text "Submit" ] ] ] ] ]

let greetingLayout name =
    match name with
    | Some name ->
        match System.String.IsNullOrEmpty(name) with
        | true -> Html.none
        | false -> Helpers.contentBox (Html.p (sprintf "Hello, %s" name))
    | None -> Html.none

let private greeting = Html.div [ prop.id "greeting" ]

let private dynamicView time =
    [ Helpers.contentBox
        (Html.a
            [ prop.className "pt-2 pb-2 pl-4 pr-3 rounded-sm bg-blue-600 text-white"
              prop.href "/"
              prop.text "Back to Main" ])
      Helpers.contentBox (fromClient time)
      Helpers.contentBox (fromServer time)
      Helpers.contentBox form
      greeting ]

let layout time =
    App.layout (Html.div [ prop.children (dynamicView time) ])
