public class Bouncer
{
    public Bar BarRef;
    public Line LineRef;
    public int Guess;

    public void Admit(Patron p) {
        BarRef.Enter(p);
    }

    public void Reject(Patron p) {
        
    }

    public void ChangeGuess(int val) {
        Guess += val;
    }

}