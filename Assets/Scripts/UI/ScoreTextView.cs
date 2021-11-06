using Game;

namespace UI
{
    public class ScoreTextView : TextView
    {
        public void OnChanged(Score score)
        {
            OnChanged(score.Max, score.Current);
        }
    }
}