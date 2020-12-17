module Program

open Saturn.Application
open Saturn.Pipeline
open Saturn.PipelineHelpers
open Saturn.Router
open Giraffe.ResponseWriters
open Giraffe.Core
open Giraffe.ModelBinding
open Microsoft.AspNetCore.Http
open Feliz.ViewEngine
open FSharp.Control.Tasks
open Model

let setTurbolinksLocationHeader: HttpHandler =
    let isTurbolink (ctx: HttpContext) =
        ctx.Request.Headers.ContainsKey "Turbolinks-Referrer"

    fun next ctx ->
        task {
            if isTurbolink ctx
            then ctx.SetHttpHeader "Turbolinks-Location" (ctx.Request.Path + ctx.Request.QueryString)

            return! next ctx
        }

let browser =
    pipeline {
        plug putSecureBrowserHeaders
        set_header "x-pipeline-type" "Browser"
        plug setTurbolinksLocationHeader
    }

let private htmx (layout: ReactElement) next (context: HttpContext) =

    let isHtmx =
        context.Request.Headers.ContainsKey("HX-Request")

    if isHtmx
    then htmlString (layout |> Render.htmlView) next context
    else htmlString (layout |> Render.htmlDocument) next context

let defaultView =
    router {
        get "/" (htmx Index.layout)
        get "/index.html" (redirectTo false "/")
        get "/default.html" (redirectTo false "/")
    }

let timeHandler next context =
    context
    |> htmx (DynamicView.timeLayout (System.DateTime.Now.ToString())) next

let dynamicViewHandler next context =
    htmx (DynamicView.layout (System.DateTime.Now.ToString())) next context

let nameHandler next (context: HttpContext) =
    let name = context.TryGetQueryStringValue "name"

    htmx (DynamicView.greetingLayout name) next context

let modelHandler next (context: HttpContext) =
    task {
        let! model = context.BindFormAsync<PostModel>()
        return! htmx (DynamicView.modelLayout model) next context
    }


let browserRouter =
    router {
        pipe_through browser

        forward "" defaultView
        get "/otherView" (htmx OtherView.layout)
        get "/dynamicView" dynamicViewHandler
        get "/time" timeHandler
        get "/name" nameHandler
        post "/model" modelHandler
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
