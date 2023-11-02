/// БАЗОВЫЙ ФУНКЦИОНАЛ ОТОБРАЖЕНИЯ

using Oxide.Game.Rust.Cui;
using System;


namespace Oxide.Plugins {
    [Info("Chat Player Pos", "Aurora", "0.0.1")]
    class PlayerPos : RustPlugin {
        private Timer updateTimer;
        private BasePlayer currentViewer;
        private string windowName = "MyVideoWindow";
        private string Min = "0.01302083 0.8796296"; // Используй CuiBuilder 
        private string Max = "0.1171875 0.9722232"; // Используй CuiBuilder 
        private string apiUrl = "http://localhost:3001/api/"; // ссылки типа (example.com/image/ и example.com/picture.png)
        
        
        
        [ChatCommand("w")] // Определение команды /w для открытия реклы
        void cmdOpenWindow(BasePlayer player) {
            currentViewer = player;
            DestroyUIWindow(player); // Убираем старое окно если есть
            CreateUIWindow(player); // создаем реклу
            updateTimer = timer.Every(5f, () => RefreshImage());
        }

        
        [ChatCommand("closewindow")]
        void cmdCloseWindow(BasePlayer player) {
            DestroyUIWindow(player);
        }

        
        private void CreateUIWindow(BasePlayer player) {
            var container = new CuiElementContainer();
            var rawImageComponent = new CuiRawImageComponent {
                Url = apiUrl,
                Color = "1 1 1 1", //rgba
                FadeIn = 1.0f // анимка
            };
            var rectTransformComponent = new CuiRectTransformComponent {
                AnchorMin = Min,
                AnchorMax = Max,
            };
            container.Add(
                new CuiElement {
                    Name = windowName, 
                    Components = {
                        rawImageComponent,
                        rectTransformComponent
                }
            });
            CuiHelper.AddUi(player, container);
        }

        private void DestroyUIWindow(BasePlayer player) {
            CuiHelper.DestroyUi(player, windowName);
        }

        private void RefreshImage() {
            if (currentViewer != null) {
                DestroyUIWindow(currentViewer);
                CreateUIWindow(currentViewer);
            }
        }
    }
}
