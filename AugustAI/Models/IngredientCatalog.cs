// Овој namespace означува дека класата припаѓа на домен моделите
// во рамки на апликацијата AugustAI.
namespace AugustAI.Models
{
    // Static класа бидејќи претставува централен каталог
    // кој не треба да се инстанцира.
    // Таа служи како read-only извор на податоци (in-memory catalog).
    public static class IngredientCatalog
    {
        // IReadOnlyDictionary се користи за да се спречи модификација
        // на структурата од надворешни класи.
        // Ова осигурува дека каталогот останува конзистентен.
        public static readonly IReadOnlyDictionary<IngredientCategory, List<string>> Items
            = new Dictionary<IngredientCategory, List<string>>
            {
                // =========================
                // PROTEINS CATEGORY
                // =========================
                // Оваа категорија содржи примарни извори на протеини.
                // Тие најчесто се основа на главни јадења.
                [IngredientCategory.Proteins] = new()
                {
                    "Chicken", "Beef", "Pork", "Lamb", "Duck", "Turkey", "Veal"
                },

                // =========================
                // SEAFOOD CATEGORY
                // =========================
                // Морска храна како посебна кулинарска група,
                // со специфични pairing правила и техники.
                [IngredientCategory.Seafood] = new()
                {
                    "White Fish", "Salmon", "Tuna", "Shrimp", "Mussels", "Octopus"
                },

                // =========================
                // FUNGI CATEGORY
                // =========================
                // Габите се одвоени од зеленчук поради нивниот
                // специфичен умами профил и кулинарска примена.
                [IngredientCategory.Fungi] = new()
                {
                    "Mushrooms", "Shiitake", "Porcini", "Oyster Mushrooms"
                },

                // =========================
                // VEGETABLES CATEGORY
                // =========================
                // Зеленчук – најширока група за гарнири,
                // основи за сосови и вегетаријански јадења.
                [IngredientCategory.Vegetables] = new()
                {
                    "Potato", "Tomato", "Onion", "Garlic", "Carrot", "Zucchini",
                    "Eggplant", "Bell Pepper", "Spinach", "Broccoli", "Cauliflower", "Asparagus"
                },

                // =========================
                // FRUITS CATEGORY
                // =========================
                // Овошје – се користи во десерти, сосови,
                // но и во контрастни солени јадења.
                [IngredientCategory.Fruits] = new()
                {
                    "Apple", "Pear", "Orange", "Lemon", "Strawberries",
                    "Blueberries", "Peach", "Pomegranate", "Fig"
                },

                // =========================
                // DAIRY CATEGORY
                // =========================
                // Млечни производи – основа за кремасти текстури
                // и емулзифицирани сосови.
                [IngredientCategory.Dairy] = new()
                {
                    "Milk", "Butter", "Cream", "Yogurt",
                    "Parmesan", "Mozzarella", "Feta", "Blue Cheese"
                },

                // =========================
                // GRAINS CATEGORY
                // =========================
                // Житарици и скробни производи – база за гарнири,
                // тестенини и теста.
                [IngredientCategory.Grains] = new()
                {
                    "Rice", "Pasta", "Bread", "Flour", "Couscous", "Quinoa", "Barley"
                },

                // =========================
                // LEGUMES CATEGORY
                // =========================
                // Мешунки – растителен протеин,
                // значајни во модерната гастрономија.
                [IngredientCategory.Legumes] = new()
                {
                    "Lentils", "Chickpeas", "White Beans", "Red Beans", "Peas"
                },

                // =========================
                // NUTS CATEGORY
                // =========================
                // Јаткасти плодови и семки – додаваат текстура
                // и маслен профил на јадењата.
                [IngredientCategory.Nuts] = new()
                {
                    "Almonds", "Walnuts", "Hazelnuts", "Pistachio",
                    "Sesame", "Sunflower Seeds"
                },

                // =========================
                // HERBS AND SPICES CATEGORY
                // =========================
                // Ароматични билки и зачини – клучни за flavor profiling.
                [IngredientCategory.HerbsAndSpices] = new()
                {
                    "Thyme", "Rosemary", "Parsley", "Basil", "Dill",
                    "Cinnamon", "Paprika", "Chili", "Black Pepper", "Cumin",
                    "Nutmeg", "Vanilla"
                },

                // =========================
                // FATS AND OILS CATEGORY
                // =========================
                // Масла и масти – неопходни за термичка обработка
                // и носители на арома.
                [IngredientCategory.FatsAndOils] = new()
                {
                    "Olive Oil", "Butter", "Sunflower Oil", "Sesame Oil", "Duck Fat"
                }
            };

        // Метод за добивање на листа на состојки според категорија.
        // Ова се користи за динамичко пополнување на dropdown менија.
        public static List<string> Get(IngredientCategory category)
            => Items.TryGetValue(category, out var list) ? list : new List<string>();

        // Метод за проверка дали одредена состојка припаѓа
        // на дадена категорија (case-insensitive проверка).
        // Ова е корисно за валидација и pairing логика.
        public static bool Contains(IngredientCategory category, string ingredient)
            => Items.TryGetValue(category, out var list)
               && list.Contains(ingredient, System.StringComparer.OrdinalIgnoreCase);
    }
}