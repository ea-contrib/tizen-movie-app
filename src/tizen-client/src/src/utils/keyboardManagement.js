export function initializeKeyboardActions(history) {
  document.addEventListener("keydown", function (x) {
    if (x.key === "Backspace" || x.keyCode === 10009) {
      //back
      history.goBack();
    } else if (x.keyCode === 13) {
      //enter
      x.target.click();
      return false;
    }
    return true;
  });
}
