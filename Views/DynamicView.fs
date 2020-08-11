module DynamicView

open Feliz.ViewEngine
open Model

let timeLayout time =
    Html.div (sprintf "Current Time is: %s" time)

let private fromClient time =
    Html.div
        [ prop.children [ Html.p "Client"
                          timeLayout time ] ]

let private fromServer time =
    Html.div
        [ prop.children [ Html.p "Server"
                          Html.div [ prop.custom ("hx-get", "/time")
                                     prop.custom ("hx-trigger", "every 1s")
                                     prop.children [ timeLayout time ] ] ] ]

let private formChild firstElement secondElement =
    Html.div [ prop.className "md:flex md:items-center mb-6"
               prop.children [ Html.div [ prop.className "md:w-1/3"
                                          prop.children firstElement ]
                               Html.div [ prop.className "md:w-2/3"
                                          prop.children secondElement ] ] ]

let private button (text: string) =
    Html.button [ prop.className
                      "shadow bg-purple-500 hover:bg-purple-400 focus:shadow-outline focus:outline-none text-white font-bold py-2 px-4 rounded"
                  prop.type' "submit"
                  prop.text text ]

let private label name (labelText: string) =
    Html.label [ prop.className "block text-gray-500 font-bold md:text-right mb-1 md:mb-0 pr-4"
                 prop.for' name
                 prop.text labelText ]

let private checkBoxInput name (labelText: string) =
    Html.label [ prop.className "md:w-2/3 block text-gray-500 font-bold"
                 prop.children [ Html.input [ prop.type' "checkbox"
                                              prop.className "mr-2"
                                              prop.name name
                                              prop.value "true"
                                              prop.id name ]
                                 Html.span [ prop.className "text-sm"
                                             prop.text labelText ] ] ]

let private singleInput name type' =
    Html.input [ prop.name name
                 prop.type' type'
                 prop.className
                     "bg-gray-200 appearance-none border-2 border-gray-200 rounded w-full py-2 px-4 text-gray-700 leading-tight focus:outline-none focus:bg-white focus:border-purple-500"
                 prop.id name ]

let private createInput name labelText type' =

    let label = label name labelText
    let input = singleInput name type'

    formChild label input

let form type' to' target (inputs: ReactElement list) =

    Html.form [ prop.custom ((sprintf "hx-%s" type'), to')
                prop.custom ("hx-target", target)
                prop.className "w-full max-w-sm"
                prop.children inputs ]

let private getForm =

    let nameInput =
        createInput "name" "Enter your name" "text"

    let sendNameButton = formChild Html.none (button "Send Name")

    let form =
        [ nameInput; sendNameButton ]
        |> form "get" "/name" "#greeting"

    Html.div
        [ prop.children [ Html.h1 "Form with GET request"
                          form ] ]

let private postForm =

    let postModelButton = formChild Html.none (button "Submit")

    let favouriteNumberInput =
        createInput "favouriteNumber" "Enter your Favourite Number" "number"

    let nameInput =
        createInput "name" "Enter your name" "text"

    let dateInput =
        createInput "date" "Enter your Date" "date"

    let isActiveInput =
        formChild Html.none (checkBoxInput "isActive" "Are you active?")

    let form =
        [ favouriteNumberInput
          nameInput
          dateInput
          isActiveInput
          postModelButton ]
        |> form "post" "/model" "#model"

    Html.div
        [ prop.children [ Html.h1 "Form with POST request"
                          form ] ]

let modelLayout (model: PostModel) =
    let name = Html.h1 (sprintf "Name: %s" model.Name)

    let favouriteNumber =
        Html.h1 (sprintf "Favourite Number: %i" model.FavouriteNumber)

    let dateString = model.Date.ToString()
    let date = Html.h1 (sprintf "Date: %s" dateString)

    let isActive =
        Html.h1 (sprintf "Is Active: %b" model.IsActive)

    let content =
        Html.div
            [ prop.children [ name
                              favouriteNumber
                              date
                              isActive ] ]

    Helpers.contentBox content

let greetingLayout name =
    match name with
    | Some name ->
        match System.String.IsNullOrEmpty(name) with
        | true -> Html.none
        | false -> Helpers.contentBox (Html.p (sprintf "Hello, %s" name))
    | None -> Html.none

let private greeting = Html.div [ prop.id "greeting" ]
let private model = Html.div [ prop.id "model" ]

let private dynamicView time =
    [ Helpers.contentBox
        (Html.a [ prop.className "pt-2 pb-2 pl-4 pr-3 rounded-sm bg-blue-600 text-white"
                  prop.href "/"
                  prop.text "Back to Main" ])
      Helpers.contentBox (fromClient time)
      Helpers.contentBox (fromServer time)
      Helpers.contentBox getForm
      greeting
      Helpers.contentBox postForm
      model ]

let layout time =
    App.layout (Html.div [ prop.children (dynamicView time) ])
