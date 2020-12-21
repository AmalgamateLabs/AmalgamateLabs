function LoadBackground() {
    var imageIndex = localStorage.getItem("ImageIndex");

    if (imageIndex == null) {
        imageIndex = 1;
    }

    SetBackground(imageIndex);
    AddChangeToMenu();
};

function ChangeBackground() {
    var imageIndex = Number(localStorage.getItem("ImageIndex"));

    if (imageIndex < 5) {
        imageIndex = imageIndex + 1;
    }
    else {
        imageIndex = 1;
    }

    SetBackground(imageIndex);
}

function SetBackground(imageIndex) {
    var imagePath = "/images/htpc/backgrounds/" + imageIndex + ".jpg";
    document.getElementsByTagName("header")[0].style.backgroundImage = "url(" + imagePath + ")";

    localStorage.setItem("ImageIndex", imageIndex);
}

function AddChangeToMenu() {
    var anchorNode = document.createElement("a");
    anchorNode.href = "javascript:ChangeBackground()";
    anchorNode.appendChild(document.createTextNode("Background"));

    var listNode = document.createElement("li");
    listNode.appendChild(anchorNode);

    document.getElementById("navigationList").appendChild(listNode);
}