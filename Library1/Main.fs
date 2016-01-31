namespace Library1
open Microsoft.FSharp.Quotations;;
open WebSharper

[<ReflectedDefinition>]
module helpers =    
    
    let rec fact1 n = 
        if n <= 1 then
            1
        else
            2 * n * fact1 (n-1)