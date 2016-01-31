namespace BisectionWeb

open WebSharper
open WebSharper.JavaScript
open WebSharper.Html.Client

//open Library1.helpers

[<JavaScript>]
module Client =

    let Start input valx k =
        async {
            let! data = Server.Eval input valx
            return k data
        }
        |> Async.Start
    

//    [<JavaScript>]
//    let updateUI =
//        JS.Window?updateUI //equation result
          

    let Main () =
        let input = Input [Attr.Value "";Id "txtEquation"] -< []
        let hdResult = Input [Attr.Value "";Id "hdResult";Type "hidden"]  -< []
        input.SetCss("width","300px")
        let output = H4 []
        Div [
            input
            hdResult
            Button [Text "Solve and Plot"]
            |>! OnClick (fun _ _ ->
                async {
                    let! result = Server.Eval input.Value 1.                    
                    hdResult.Value <- result                 
                    
                }
                |> Async.Start
            )
            HR []
            //H4 [Attr.Class "text-muted"] -< [Text "Result:"]
            //Div [Attr.Class "jumbotron"] -< [output]
            
        ]
