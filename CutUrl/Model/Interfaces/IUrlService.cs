namespace CutUrl.Model.Interfaces
{
    public interface IUrlService
    {
        /// <summary>
        /// Создает сокращенный URL по полному.
        /// </summary>
        /// <param name="fullURL">Полный URL.</param>
        /// <returns>Сокращенный URL.</returns>
        string Create(string fullURL);
    }
}
