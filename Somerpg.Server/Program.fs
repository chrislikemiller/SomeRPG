open System
open System.Threading
open Suave
open Suave.Json
open Suave.Operators
open Suave.Filters
open Suave.Successful
open Somerpg.Server.Model

let jsonHandler : WebPart = 
    mapJson (fun (model:TestModel) -> {model with Message = model.Message + "Hello"})

let httpHandler : WebPart = 
    choose 
        [ GET >=> choose 
            [ path "/" >=> request (fun r -> OK ("test") ) ]
        ]

[<EntryPoint>]
let main argv =
    let cts = new CancellationTokenSource()
    let ip = "0.0.0.0"
    let port = 8080
    let conf = { defaultConfig with 
                    cancellationToken = cts.Token; 
                    bindings = [ HttpBinding.createSimple Protocol.HTTP ip port] }
    let _, server = startWebServerAsync conf jsonHandler
  
    Async.Start(server, cts.Token)
    Console.ReadKey true |> ignore  
    cts.Cancel()
    0