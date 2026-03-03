function updatePreview() {
    const category = document.getElementById("categorySelect").value;
    const ingredientEl = document.getElementById("ingredientSelect");
    const ingredient = ingredientEl && ingredientEl.value ? ingredientEl.value : "—";
    document.getElementById("selectionPreview").textContent = `${category} → ${ingredient}`;
}

async function loadIngredients() {
    const category = document.getElementById("categorySelect").value;
    const ingredientSelect = document.getElementById("ingredientSelect");

    ingredientSelect.innerHTML = "<option>Loading...</option>";

    try {
        const response = await fetch(`/Home/IngredientsByCategory?category=${encodeURIComponent(category)}`);
        const data = await response.json();

        ingredientSelect.innerHTML = "";

        const placeholder = document.createElement("option");
        placeholder.value = "";
        placeholder.textContent = "Select ingredient";
        placeholder.selected = true;
        placeholder.disabled = true;
        ingredientSelect.appendChild(placeholder);

        data.forEach(item => {
            const option = document.createElement("option");
            option.value = item;
            option.textContent = item;
            ingredientSelect.appendChild(option);
        });

        updatePreview();
    } catch (error) {
        console.error("Error loading ingredients:", error);
        ingredientSelect.innerHTML = "<option>Error loading data</option>";
        updatePreview();
    }
}

function addIngredient() {
    const ingredientSelect = document.getElementById("ingredientSelect");
    const selectedValue = ingredientSelect.value;

    if (!selectedValue) return;

    const list = document.getElementById("ingredientList");

    const exists = Array.from(list.querySelectorAll("li"))
        .some(li => li.dataset.value === selectedValue);

    if (exists) return;

    const li = document.createElement("li");
    li.className = "list-group-item d-flex justify-content-between align-items-center";
    li.dataset.value = selectedValue;

    const text = document.createElement("span");
    text.textContent = selectedValue;

    const btn = document.createElement("button");
    btn.type = "button";
    btn.className = "btn btn-sm btn-outline-danger";
    btn.textContent = "Remove";
    btn.addEventListener("click", () => {
        li.remove();
        syncIngredientsToHidden();
    });
    li.appendChild(text);
    li.appendChild(btn);
    list.appendChild(li);
    syncIngredientsToHidden();
}

document.addEventListener("DOMContentLoaded", () => {
    document.getElementById("categorySelect").addEventListener("change", onCategoryChanged);
    document.getElementById("ingredientSelect").addEventListener("change", updatePreview);
    document.getElementById("addIngredientBtn").addEventListener("click", addIngredient);
    document.getElementById("clearAllBtn").addEventListener("click", clearList);
    loadIngredients();
    syncIngredientsToHidden();
});
function syncIngredientsToHidden() {
    const list = document.getElementById("ingredientList");
    const values = Array.from(list.querySelectorAll("li")).map(li => li.dataset.value);
    document.getElementById("ingredientsJson").value = JSON.stringify(values);
}
function clearList() {
    document.getElementById("ingredientList").innerHTML = "";
    syncIngredientsToHidden();
}

function onCategoryChanged() {
    clearList();
    loadIngredients();
}

