open System
open System.Text.Json
open SixLabors.ImageSharp
open SixLabors.ImageSharp.Processing
open SixLabors.ImageSharp.PixelFormats
open Accord.MachineLearning

let loadImage (size:Size) (path:string) =
    use img = Image.Load path : Image<L8>
    img.Mutate(fun i -> i.Resize(size) |> ignore)
    let pixels = Array.create (size.Width * size.Height) (L8 0uy)
    img.CopyPixelDataTo pixels
    pixels |> Array.map (fun i -> float i.PackedValue)

let processData size numberOfClusters imageFiles =
    let images = Array.map (loadImage size) imageFiles
    let classifier = (KMeans numberOfClusters).Learn images
    Array.zip images imageFiles
    |> Array.groupBy (fst >> classifier.Decide)
    |> Array.map (snd >> Array.map snd)

let toJson clusters =
    let opt = JsonSerializerOptions()
    opt.WriteIndented <- true
    JsonSerializer.Serialize(clusters, opt)

[<EntryPoint>]
let main args =
    match args with
    | [| w; h; clustersText |] ->
        let size = Size(Int32.Parse w, Int32.Parse h)
        let clusters = Int32.Parse clustersText
        Console.In.ReadToEnd().Split(Environment.NewLine, StringSplitOptions.RemoveEmptyEntries)
        |> processData size clusters
        |> toJson
        |> printfn "%s"
        0
    | _ ->
        printfn "Usage:"
        printfn "clumages resizeToWidth resizeToHeight numberOfClusters < list-of-files.txt"
        printfn "clumages 32 32 10 < list-of-files.txt"
        printfn "ls data/*.png | clumages 32 32 10 | jq -r '.[][0]'"
        -1
