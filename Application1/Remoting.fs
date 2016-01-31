namespace BisectionWeb

open WebSharper

open System.Linq
open System.Collections
open System.Collections.Generic
open System.Linq.Expressions

open System.Numerics
open MathNet.Numerics
open MathNet.Symbolics
open MathNet.Numerics.LinearAlgebra
open Operators

module Server =

    [<Rpc>]
    let DoSomething input =
        let R (s: string) = System.String(Array.rev(s.ToCharArray()))
        async {
            return R input
        }
    
    
    let EvalEquation (eqautionText:string) (xval:float) = 
        let x = symbol "x"
        let symbols = Map.ofList [ "x", FloatingPoint.Real(xval); ]
        // https://github.com/mathnet/mathnet-symbolics/blob/fba141757985e30e90cae7e73c951246c661cf5b/src/Symbolics/Infix.fs
        let equation = Infix.parse eqautionText
        match equation with
        | ParsedExpression xp -> 
            //https://github.com/mathnet/mathnet-symbolics/blob/master/src/Symbolics/Evaluate.fs
            let result = Evaluate.evaluate symbols xp  
            result.RealValue
        | _ -> failwith "Invalid equation"
    

    let sign x = 
        if x > 0. then 
            Some(true)
        elif x < 0. then
            Some(false)
        else
            None

    let rec bisection (a:float) (b:float) (f: float -> float) (tolerance:float) (maxIter:int) (history:float list) = 

        if maxIter <= 0 then
            failwith "Exceeded max iterations"
    
        if (sign (f a))<>None && (sign (f a)) = (sign (f b) ) then
            failwith "Initial points evaluated function are of same sign"

    
        
        let a' = min a b
        let b' = max a b

   
        let  c = (a' + b') / 2.    
        if (f c) = 0. || (b'-a') < tolerance then
            history @ [c]
        else
            let updatedHistory = history @ [c]
            if sign(f c) = sign(f a') then                
                bisection c b' f tolerance (maxIter-1) updatedHistory
            else                
                bisection a' c f tolerance (maxIter-1) updatedHistory

    let genRandomNumbers range count =
        let rnd = System.Random()
        List.init count (fun _ -> 
            let sign1 = 
                match (rnd.NextDouble() > 0.5) with
                | true -> 1.
                | false -> -1.

            let sign2 = 
                match (rnd.NextDouble() > 0.5) with
                | true -> 1.
                | false -> -1.

            (sign1 * range * rnd.NextDouble() , sign2 * range * rnd.NextDouble())
        )


    let rec findInitialPoints (f: float -> float) range  =    
        let randoms = genRandomNumbers range 1000
        let pair = randoms |> List.tryFind (fun p ->
            let a,b = p 
            if sign (f a) <> None && sign (f a) <> sign (f b) then true
            else    false 
        )

        match pair with
        | Some(a,b) -> pair
        | None -> failwith "Cannot find initial two points"
      
    

    let FindRoot (equationText:string) (xval:float) = 
        try
            let initialPoints = findInitialPoints (EvalEquation equationText) 100.
            let a,b = initialPoints.Value
            let history = [a;b]
            let rootList = bisection a b (EvalEquation equationText) 0.00001 100 history
     
            let result = rootList |> Seq.map (fun a -> a.ToString()) |> Seq.reduce (fun a b -> a.ToString() + ";" + b.ToString())
            //System.DateTime.Now.ToString()
            result
        with
            | :? System.Exception as ex -> ex.Message


        

    [<Rpc>]
    let Eval (eqautionText:string) (xval:float) =         
        let R (p1: string) (p2: float)  = FindRoot p1 p2
        async {
            return R eqautionText xval
        }