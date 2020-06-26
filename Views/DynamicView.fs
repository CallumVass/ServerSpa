module DynamicView

open Giraffe.GiraffeViewEngine

let timeLayout time =
    div [] [ str (sprintf "Current Time is: %s" time) ]

let private fromClient time =
    div []
        [ p [] [ str "Client" ]
          timeLayout time ]

let private fromServer time =
    div []
        [ p [] [ str "Server" ]
          div
              [ attr "hx-get" "/time"
                attr "hx-trigger" "every 1s" ] [ timeLayout time ] ]

let private form =
    form
        [ _class "w-full max-w-sm"
          attr "hx-get" "/name"
          attr "hx-target" "#greeting" ]
        [ div [ _class "flex items-center border-b border-b-2 border-purple-500 py-2" ]
              [ input
                  [ _name "name"
                    _placeholder "Enter your name"
                    _class
                        "appearance-none bg-transparent border-none w-full text-gray-700 mr-3 py-1 px-2 leading-tight focus:outline-none" ]
                button
                    [ _class
                        "flex-shrink-0 bg-purple-500 hover:bg-purple-700 border-purple-500 hover:border-purple-700 text-sm border-4 text-white py-1 px-2 rounded"
                      _type "submit" ] [ str "Submit" ] ] ]

let greetingLayout name =
    match name with
    | Some name ->
        match System.String.IsNullOrEmpty(name) with
        | true -> div [] []
        | false -> Helpers.contentBox (p [] [ str (sprintf "Hello, %s" name) ])
    | None -> div [] []

let private greeting = div [ _id "greeting" ] []

let private dynamicView time =
    [ Helpers.contentBox
        (a
            [ _class "pt-2 pb-2 pl-4 pr-3 rounded-sm bg-blue-600 text-white"
              _href "/" ] [ str "Back to Main" ])
      Helpers.contentBox (fromClient time)
      Helpers.contentBox (fromServer time)
      Helpers.contentBox form
      greeting ]

let layout time = App.layout (dynamicView time)
