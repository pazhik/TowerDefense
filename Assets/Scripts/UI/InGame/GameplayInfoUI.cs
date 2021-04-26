using System;
using RunTime;
using UnityEngine;
using UnityEngine.UI;

namespace UI.InGame
{
    public class GameplayInfoUI: MonoBehaviour
    {
        [SerializeField] private Text m_Health;
        [SerializeField] private Text m_Money;
        [SerializeField] private Text m_Waves;

        private void OnEnable()
        {
            SetHealth(Game.Player.Health);
            SetMoney(Game.Player.TurretMarket.Money);
            
            Game.Player.HealthChanged += SetHealth;
            Game.Player.TurretMarket.MoneyChanged += SetMoney;
        }

        private void OnDisable()
        {
            Game.Player.HealthChanged -= SetHealth;
            Game.Player.TurretMarket.MoneyChanged -= SetMoney;
        }

        private void SetHealth(int health)
        {
            m_Health.text = $"Health: {health}";
        }
        
        private void SetMoney(int money)
        {
            m_Money.text = $"Money : {money}";
        }
    }
}