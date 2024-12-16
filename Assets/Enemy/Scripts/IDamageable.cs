/// Интерфейс для объектов, которые могут получать урон.
public interface IDamageable
{
    /// Метод для получения урона.
    /// <param name="damage">Количество наносимого урона.</param>
    void TakeDamage(int damage);
}