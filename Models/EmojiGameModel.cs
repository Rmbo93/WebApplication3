namespace WebApplication3.Models

{
    public class EmojiGameModel
    {
        


        public class AnimalModel
        {
            public List<string> AnimalEmoji { get; set; }

            public AnimalModel()
            {
                AnimalEmoji = new List<string>
            {
                "🐶", "🐶",
                "🐹", "🐹",
                "🐁", "🐁",
                "🦊", "🦊",
                "🐺", "🐺",
                "🦌", "🦌",
                "🦄", "🦄",
                "🐲", "🐲"
            };
            }
        }

        public class FoodModel
        {
            public List<string> FoodEmoji { get; set; }

            public FoodModel()
            {
                FoodEmoji = new List<string>
            {
                "🍔", "🍔",
                "🍕", "🍕",
                "🌯", "🌯",
                "🌮", "🌮",
                "🐟", "🐟",
                "🍚", "🍚",
                "🍣", "🍣",
                "🍤", "🍤"
            };
            }
        }


        public class FruitModel
        {
            public List<string> FruitEmoji { get; set; }

            public FruitModel()
            {
                FruitEmoji = new List<string>
            {
                "🍉", "🍉",
                "🍓", "🍓",
                "🥝", "🥝",
                "🍑", "🍑",
                "🍎", "🍎",
                "🥭", "🥭",
                "🍌", "🍌",
                "🥥", "🥥"
            };
            }
        }
        public class UserModel
        {
            public string Username { get; set; }
            public int GamesPlayed { get; set; }

            public UserModel()
            {
                Username = string.Empty;
                GamesPlayed = 0;
            }
        }
    }



}

