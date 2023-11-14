using System.Threading.Tasks;

namespace Hero.Script.InGame.Jerk
{
    public interface IHeroJerk
    {
        float jerkSpeed { get; }
        void DoJerk();
        void EndJerk();
    }
}