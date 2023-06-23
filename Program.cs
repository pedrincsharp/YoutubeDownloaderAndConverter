using System;
using System.Threading.Tasks;
using YoutubeDownloaderAndConverter.YDC;

namespace YoutubeDownloaderAndConverter
{
    class Program
    {
        static DownloadVideo download;
        static async Task Main(string[] args)
        {
            Console.WriteLine("Digite a URL do video:");
            string url = Console.ReadLine();

            if (string.IsNullOrWhiteSpace(url))
                Console.WriteLine("Url vazia informada!");

            Console.WriteLine("Informe o formato (MP3 - MP4)\nPadrão é MP4 (Deixar vazio)");
            string formato = Console.ReadLine();

            if (string.IsNullOrWhiteSpace(formato))
                formato = "MP4";

            formato = formato.ToUpper();

            download = new DownloadVideo(url);
            await download.GetVideo("C:\\Videos", formato);

            Console.WriteLine("Presione qualquer tecla para sair......");
            Console.ReadKey(true);
        }
    }
}
