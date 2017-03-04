using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.GamerServices;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;
namespace FrameWork.FrameWork.Audio
{
    class Audio
    {
        static Audio _Instance;

        #region fields
        AudioEngine _audioEngine;
        WaveBank _waveBank;
        SoundBank _soundBank;
        List<Cue> Backs;
        bool _isEff, _isBack;
        #endregion

        public bool Effect
        {
            get { return _isEff; }
            set { _isEff = value; }
        }
        public bool BackGround
        {
            get { return _isBack; }
            set { _isBack = value; }
        }
        Audio()
        {
            _audioEngine = new AudioEngine(@"Content\Audio\MarioAudio.xgs");
            _waveBank = new WaveBank(_audioEngine, @"Content\Audio\Wave Bank.xwb");
            _soundBank = new SoundBank(_audioEngine, @"Content\Audio\Sound Bank.xsb");
            Backs = new List<Cue>();
            _isEff = true; _isBack = true;

        }
        public static Audio Instance()
        {
            if (_Instance == null)
            {
                _Instance = new Audio();
            }
            return _Instance;
        }
        public void Play(string NameSound)
        {
            if (NameSound.ToCharArray()[0] == 'e' && _isEff)
            {
                _soundBank.PlayCue(NameSound);
            }
            if (NameSound.ToCharArray()[0] == 'b' && _isBack)
            {
                Cue cue = _soundBank.GetCue(NameSound);
                cue.Play();
                Backs.Add(cue);
            }

        }

        public void StopAllBack()
        {
            foreach (Cue cue in Backs)
            {
                cue.Stop(AudioStopOptions.AsAuthored);
            }
            Backs.Clear();
        }

        public void Stop(string NameSound)
        {
            Cue cue = _soundBank.GetCue(NameSound);
            cue.Stop(AudioStopOptions.Immediate);
        }
    }
}
