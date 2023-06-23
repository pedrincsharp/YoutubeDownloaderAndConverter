using System;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using YoutubeExplode;
using YoutubeExplode.Videos;
using YoutubeExplode.Videos.Streams;

namespace YoutubeDownloaderAndConverter.YDC
{
    public class DownloadVideo
    {
        private string _url;
        private YoutubeClient _client;

        public DownloadVideo(string url)
        {
            _url = url;
            _client = new YoutubeClient();
        }

        public async Task<bool> GetVideo(string savePath, string formato)
        {
            try
            {
                Video video = await _client.Videos.GetAsync(_url);
                if (video == null)
                    throw new Exception("Video inválido ou URL informada é inválida!");
                await SaveVideo(video, savePath, formato);
                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
                throw;
            }
        }

        private async Task SaveVideo(Video video, string path, string formato)
        {
            StreamManifest streamManifest = await _client.Videos.Streams.GetManifestAsync(_url);
            IVideoStreamInfo videoFormat = null;
            IStreamInfo musicFormat = null;

            if (formato == "MP4")
            {
                videoFormat = streamManifest.GetVideoOnlyStreams()
                    .Where(s => s.Container == Container.Mp4)
                    .GetWithHighestVideoQuality();
            }
            else
            {
                musicFormat = streamManifest.GetAudioOnlyStreams().GetWithHighestBitrate();
            }


            Console.WriteLine("Fazendo download....");

            await _client.Videos.Streams
                .DownloadAsync(videoFormat != null ? videoFormat : musicFormat,
                Path.Combine(path, $"{video.Title.Substring(0, 8)}." +
                $"{(videoFormat != null ? videoFormat.Container : musicFormat.Container)}"));
        }

    }
}
