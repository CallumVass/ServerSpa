module Program

open Saturn.Application
open Saturn.Pipeline
open Saturn.PipelineHelpers
open Saturn.Router
open Giraffe.ResponseWriters
open Giraffe.Core
open Microsoft.AspNetCore.Http

let browser =
    pipeline {
        plug putSecureBrowserHeaders
        set_header "x-pipeline-type" "Browser"
    }

let defaultView =
    router {
        get "/" (htmlView Index.layout)
        get "/index.html" (redirectTo false "/")
        get "/default.html" (redirectTo false "/")
    }

let timeHandler next context =
    htmlView (DynamicView.timeLayout (System.DateTime.Now.ToString())) next context

let dynamicViewHandler next context =
    htmlView (DynamicView.layout (System.DateTime.Now.ToString())) next context

let nameHandler next (context: HttpContext) =
    let name = context.TryGetQueryStringValue "name"

    htmlView (DynamicView.greetingLayout name) next context

let browserRouter =
    router {
        pipe_through browser

        forward "" defaultView
        get "/otherView" (htmlView OtherView.layout)
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
