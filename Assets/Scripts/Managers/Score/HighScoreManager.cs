﻿using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using BeatGame.Data;
using System.Linq;
using BeatGame.Data.Score;
using Newtonsoft.Json;

namespace BeatGame.Logic.Managers
{
    public class HighScoreManager : MonoBehaviour
    {
        public static HighScoreManager Instance;

        public List<HighScoreData> HighScores;

        private void Awake()
        {
            if (Instance == null)
                Instance = this;
        }

        // Use this for initialization
        void Start()
        {
            if (PlayerPrefs.HasKey("HighScores"))
            {
                LoadScores();
            }
            else
            {
                HighScores = new List<HighScoreData>();
            }
        }

        public HighScoreData GetHighScoreForSong(string songName, string levelAuthor, string Difficulty)
        {
            var songData = new HighScoreData
            {
                SongName = songName,
                LevelAuthor = levelAuthor,
                Difficulty = Difficulty,
                Score = 0
            };

            for (int i = 0; i < HighScores.Count; i++)
            {
                if (HighScores[i].Equals(songData))
                {
                    return HighScores[i];
                }
            }

            return songData;
        }

        public void UpdateScore(HighScoreData scoreData)
        {
            for (int i = 0; i < HighScores.Count; i++)
            {
                if (HighScores[i].Equals(scoreData))
                {
                    HighScores[i] = scoreData;
                    SaveScores();
                    return;
                }
            }
            HighScores.Add(scoreData);
            SaveScores();
        }

        public void SaveScores()
        {
            PlayerPrefs.SetString("HighScores", JsonConvert.SerializeObject(HighScores.ToArray()));
            PlayerPrefs.Save();
        }

        public void LoadScores()
        {
            HighScores = new List<HighScoreData>(JsonConvert.DeserializeObject<HighScoreData[]>(PlayerPrefs.GetString("HighScores")));

        }
    }
}