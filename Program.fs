module Program

open Saturn.Application
open Saturn.Pipeline
open Saturn.PipelineHelpers
open Saturn.Router
open Giraffe.ResponseWriters
open Giraffe.Core
open Microsoft.AspNetCore.Http
open Feliz.ViewEngine

let browser =
    pipeline {
        plug putSecureBrowserHeaders
        set_header "x-pipeline-type" "Browser"
    }

let private fullHtml layout =
    htmlString (layout |> Render.htmlDocument)

let private partialHtml (layout: ReactElement) = htmlString (layout |> Render.htmlView)

let defaultView =
    router {
        get "/" (fullHtml Index.layout)
        get "/index.html" (redirectTo false "/")
        get "/default.html" (redirectTo false "/")
    }

let timeHandler next context =
    partialHtml (DynamicView.timeLayout (System.DateTime.Now.ToString())) next context

let dynamicViewHandler next context =
    fullHtml (DynamicView.layout (System.DateTime.Now.ToString())) next context

let nameHandler next (context: HttpContext) =
    let name = context.TryGetQueryStringValue "name"

    partialHtml (DynamicView.greetingLayout name) next context

let browserRouter =
    router {
        pipe_through browser

        forward "" defaultView
        get "/otherView" (fullHtml OtherView.layout)
        get "/dynamicView" dynamicViewHandler
        get "/time" timeHandler
        get "/name" nameHandler
    }


let app =
    application {
        use_router browserRouter
        use_gzip
    }

[<EntryPoint>]
let main _ =
    run app
    0 // return an integer exit code
