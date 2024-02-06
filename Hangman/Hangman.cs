using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Animations;
using UnityEngine.SceneManagement;

public class Hangman : MonoBehaviour
{

    public GameObject WordInput;
    public GameObject LetterInput;
    public GameObject CurrentWord;
    public GameObject GuessWord;
    public GameObject WinScreen;
    public GameObject LoseScreen;
    public Animator CharacterController;
    public Text WordShow;
    public Text Lives;
    public List<char> wordShow = new();

    private string word;
    private char guess;
    private float characterLives = 0.0f;

    public int lives = 7;

    public void RestartGame()
    {
        SceneManager.LoadScene(0);
    }

    private void OnEnable()
    {
        UpdateWordShow('^', word);
        WordInput.SetActive(true);
        LetterInput.SetActive(false);
        CurrentWord.SetActive(true);
        GuessWord.SetActive(false);
        WinScreen.SetActive(false);
    }

    public void WordSubmit(string w) //When Player enters a WORD
    {
        Debug.Log("Word Inputted");
        WordInput.SetActive(false);
        word = w;
        Debug.Log(word);
        InitialiseWordShow(w);
        WordShow.text = string.Join(" ", UpdateWordShow(guess, word));
        LetterInput.SetActive(true);
        GuessWord.SetActive(true);
    }
    public void GuessSubmit(string letter) //When Player enters a LETTER
    {
        LetterInput.SetActive(false);
        guess = char.Parse(letter);
        Debug.Log(guess);
        Turn();
    }
    public void GuessWordSubmit(string w) //When Player guesses the FULL word
    {
        if (word == w)
        {
            Debug.Log("Player Won");
            WinScreen.SetActive(true);
        }
        else
        {
            PlayerLose();
        }
    }

    public void Turn() //Runs once per turn, Checks if guess is correct
    {
        if (CheckIn(guess, word))
        {
            Debug.Log("Yes");
            SuccessfullGuess();
        }
        else
        {
            Debug.Log("No");
            if (lives > 1)
            {
                lives--;
                Lives.text = "Lives: " + lives.ToString();
                LetterInput.SetActive(true);
                characterLives += 0.15f;
                CharacterController.SetFloat("State", characterLives);
            }
            else
            {
                PlayerLose();
            }
        }
    }

    private void SuccessfullGuess() //Runs on a successful LETTER guess from Player
    {
        string show = string.Join(" ", UpdateWordShow(guess, word));
        WordShow.text = show;
        if (CheckIn('_', show))
        {
            LetterInput.SetActive(true);
        }
        else
        {
            PlayerWin();
        }
    }

    private void PlayerWin() //Win screen + Reset?
    {
        LetterInput.SetActive(false);
        CurrentWord.SetActive(false);
        WinScreen.SetActive(true);
    }
    private void PlayerLose() //Loss screen + Reset?
    {
        LetterInput.SetActive(false);
        CurrentWord.SetActive(false);
        LoseScreen.SetActive(true);
    }

    public bool CheckIn(char check, string container) //Check for character in a string
    {
        int i = 0;
        Debug.Log("Checking");
        while (i < container.Length)
        {
            if (check == container[i])
            {
                Debug.Log("Found Letter");
                return true;
            }
            Debug.Log("i:" + i);
            i++;
        }

        Debug.Log("Not Found");
        return false;
    }
    public List<char> UpdateWordShow(char check, string container) //Update the word hints box.
    {
        List<char> indexes = new(); 
        int i = 0;
        Debug.Log("Checking");
        while (i < container.Length)
        {
            if (check == container[i])
            {
                Debug.Log("Found Letter");
                indexes.Add(check);
                wordShow[i] = check;
                Debug.Log(indexes);
            }
            else
            {
                Debug.Log("Not here");
                indexes.Add(wordShow[i]);
                Debug.Log(indexes);
            }
            Debug.Log("i:" + i);
            i++;
        }

        Debug.Log("Not Found");
        return indexes;
    }

    public void InitialiseWordShow(string w) 
    {
        int i = 0;
        while (i < w.Length)
        {
            wordShow.Add('_');
            i++;
        }
    }
}
