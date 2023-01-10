namespace Video.Server;

public interface ITakePhotosService
{
    Task<byte[]> CameraAsync(int id);
}
