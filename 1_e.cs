using System;

// Target Interface (MediaPlayer)
public interface IMediaPlayer
{
    void Play(string mediaType, string fileName);
}

// Adaptee Class (AdvancedMediaPlayer)
public interface IAdvancedMediaPlayer
{
    void PlayVlc(string fileName);
    void PlayMp4(string fileName);
}

// Concrete Adaptee for playing VLC files
public class VlcPlayer : IAdvancedMediaPlayer
{
    public void PlayVlc(string fileName)
    {
        Console.WriteLine($"Playing VLC file: {fileName}");
    }

    public void PlayMp4(string fileName) { /* Not supported */ }
}

// Concrete Adaptee for playing MP4 files
public class Mp4Player : IAdvancedMediaPlayer
{
    public void PlayVlc(string fileName) { /* Not supported */ }

    public void PlayMp4(string fileName)
    {
        Console.WriteLine($"Playing MP4 file: {fileName}");
    }
}

// Adapter Class
public class MediaAdapter : IMediaPlayer
{
    private IAdvancedMediaPlayer _advancedMediaPlayer;

    public MediaAdapter(string mediaType)
    {
        if (mediaType.Equals("vlc"))
        {
            _advancedMediaPlayer = new VlcPlayer();
        }
        else if (mediaType.Equals("mp4"))
        {
            _advancedMediaPlayer = new Mp4Player();
        }
    }

    public void Play(string mediaType, string fileName)
    {
        if (mediaType.Equals("vlc"))
        {
            _advancedMediaPlayer.PlayVlc(fileName);
        }
        else if (mediaType.Equals("mp4"))
        {
            _advancedMediaPlayer.PlayMp4(fileName);
        }
    }
}

// Concrete MediaPlayer that uses the Adapter
public class AudioPlayer : IMediaPlayer
{
    private MediaAdapter _mediaAdapter;

    public void Play(string mediaType, string fileName)
    {
        if (mediaType.Equals("mp3"))
        {
            Console.WriteLine($"Playing MP3 file: {fileName}");
        }
        else if (mediaType.Equals("vlc") || mediaType.Equals("mp4"))
        {
            _mediaAdapter = new MediaAdapter(mediaType);
            _mediaAdapter.Play(mediaType, fileName);
        }
        else
        {
            Console.WriteLine("Invalid media type");
        }
    }
}

class Program
{
    static void Main(string[] args)
    {
        AudioPlayer audioPlayer = new AudioPlayer();
        audioPlayer.Play("mp3", "song.mp3");
        audioPlayer.Play("mp4", "video.mp4");
        audioPlayer.Play("vlc", "movie.vlc");
    }
}
