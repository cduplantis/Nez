namespace Engen

open System
open System.IO
open System.Reflection
open System.Runtime.InteropServices


type Mapping =
    { Destination: string
      Os: OSPlatform 
      Target: string }


module XPlatform =
    [<DllImport("kernel32.dll", CharSet = CharSet.Auto, SetLastError = true)>]
    extern bool SetDllDirectory(string lpPathName)

    [<DllImport("Kernel32.dll", CharSet = CharSet.Auto, SetLastError = true)>]
    extern IntPtr LoadLibrary(string lpFileName)

    [<DllImport("kernel32.dll", CharSet = CharSet.Unicode, SetLastError = true)>]
    extern bool SetDefaultDllDirectories(int directoryFlags)

    [<DllImport("kernel32.dll", CharSet = CharSet.Unicode, SetLastError = true)>]
    extern void AddDllDirectory(string lpPathName)

    let MapNatives (libraryName: string, assembly: Assembly, dllImportSearchPath: DllImportSearchPath) =
        let mappings =
            [ { Destination = "SDL2"
                Os = OSPlatform.Windows
                Target = "SDL2.dll" }
              { Destination = "SDL2"
                Os = OSPlatform.OSX
                Target = "libSDL2-2.0.0.dylib" }
              { Destination = "SDL2"
                Os = OSPlatform.FreeBSD
                Target = "libSDL2-2.0.so.0" }
              { Destination = "SDL2"
                Os = OSPlatform.Linux
                Target = "libSDL2-2.0.so.0" }
              { Destination = "Refresh"
                Os = OSPlatform.Windows
                Target = "Refresh.dll" }
              { Destination = "Refresh"
                Os = OSPlatform.OSX
                Target = "libRefresh.0.dylib" }
              { Destination = "Refresh"
                Os = OSPlatform.FreeBSD
                Target = "libRefresh.so.0" }
              { Destination = "Refresh"
                Os = OSPlatform.Linux
                Target = "libRefresh.so.0" }
              { Destination = "FAudio"
                Os = OSPlatform.Windows
                Target = "FAudio.dll" }
              { Destination = "FAudio"
                Os = OSPlatform.OSX
                Target = "libFAudio.0.dylib" }
              { Destination = "FAudio"
                Os = OSPlatform.FreeBSD
                Target = "libFAudio.so.0" }
              { Destination = "FAudio"
                Os = OSPlatform.Linux
                Target = "libFAudio.so.0" } ]

        let logTarget x : Mapping =
            printfn "Mapped %s to %s on %s OS" x.Destination (string x.Os) x.Target
            x

        let mapping =
            mappings
            |> Seq.filter
                (fun x ->
                    RuntimeInformation.IsOSPlatform(x.Os)
                    && libraryName = x.Destination)
            |> Seq.map (logTarget)
            |> Seq.head

        NativeLibrary.Load mapping.Destination

    let Init =
        let subDir =
            if Environment.Is64BitProcess then
                "x64"
            else
                "x86"

        let path =
            Path.Combine(AppDomain.CurrentDomain.BaseDirectory, subDir)

        let LOAD_LIBRARY_SEARCH_DEFAULT_DIRS = 0x00001000

        match Environment.OSVersion.Platform with
        | PlatformID.Win32NT ->
            if not (SetDllDirectory path) then
                printf "Unable to change DLL directory to '%s'" path

            try
                if not (SetDefaultDllDirectories LOAD_LIBRARY_SEARCH_DEFAULT_DIRS) then
                    printf "Unable to SetDefaultDllDirectories on"
            with _ -> AddDllDirectory path // Pre-Windows 7, KB2533623

            ()
        | _ -> ()

        let assembly = Assembly.GetAssembly typedefof<Mapping>
        ()
