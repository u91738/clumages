# Clumages
CLI tool to clusterize images with K-Means.

Call `dotnet build` to build.

Does not do any smart feature extraction. Only reads images and resizes them to N by M grayscale before feeding to k-means.

Works surprisingly well for grouping similar UI screenshots during recon when working on bug bounties,
but there is nothing that makes it specialized for such use, it's just a wrapper to run k-means from command line.

## Usage

```
$ ./clumages --help
Usage:
clumages resizeToWidth resizeToHeight numberOfClusters < list-of-files.txt
clumages 32 32 10 < list-of-files.txt
ls data/*.png | clumages 32 32 10 | jq -r '.[][0]'
```

```
$ ls test/data/*.png | ./clumages 32 32 10
[
  [
    "test/data/0.png",
    "test/data/21.png",
    "test/data/22.png",
    "test/data/23.png",
    "test/data/24.png",
  ],
  [
    "test/data/10.png",
    "test/data/11.png",
    "test/data/17.png",
    "test/data/18.png",
    "test/data/19.png",
    "test/data/2.png",
    "test/data/5.png",
    "test/data/6.png",
    "test/data/9.png"
  ],
  [
    "test/data/12.png",
    "test/data/16.png",
    "test/data/4.png"
  ],
  [
    "test/data/13.png"
  ],
  [
    "test/data/14.png",
  ],
  [
    "test/data/15.png",
    "test/data/8.png"
  ],
  [
    "test/data/1.png",
  ],
  [
    "test/data/25.png",
    "test/data/27.png",
    "test/data/28.png",
    "test/data/29.png",
  ],
  [
    "test/data/3.png"
  ],
  [
    "test/data/7.png"
  ]
]
```
