const inputs = document.querySelectorAll(".input");

function addclass() {
	let parent = this.parentNode.parentNode;
	parent.classList.add("focus");
}

function remclass() {
	let parent = this.parentNode.parentNode;
	if (this.value == "") {
		parent.classList.remove("focus");
	}
}

inputs.forEach(input => {
	input.addEventListener("focus", addclass);
	input.addEventListener("blur", remclass);
	if (input.value != "") {
		input.parentNode.parentNode.classList.add("focus");
	}
});
