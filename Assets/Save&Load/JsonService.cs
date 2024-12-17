
public interface JsonService {
    void SaveData<T>(string ReletivePath, T data);

    T LoadData<T>(string ReletivePath);
}
