const skipShiftBottomNavigationClass = "js-skip-shift-bottom-navigation";
const skipShiftTopNavigationClass = "js-skip-shift-top-navigation";
const skipShiftLeftNavigationClass = "js-skip-shift-left-navigation";
const skipShiftRightNavigationClass = "js-skip-shift-right-navigation";
const preferMeNavigationClass = "js-prefer-me-navigation";

export const tempFocusOnTopLink = () => {
  const entryPoint = document.getElementById("entry-point");
  if (entryPoint) {
    console.log(entryPoint);
    entryPoint.focus();
  }
}

// const getEffectiveZIndex = (elm) =>  {
//     let elementZIndex,
//         style,
//         zIndex = 0;
//
//     while(elm) {
//         style = getComputedStyle(elm);
//
//         if (style) {
//             elementZIndex = parseInt(style.getPropertyValue('z-index'), 10);
//
//             if (elementZIndex > zIndex) {
//                 zIndex = elementZIndex;
//             }
//         }
//
//         elm = elm.parentNode;
//     }
//
//     return zIndex;
// };

const getFocusableElements = () => {
  let focusableElements = Array.from(
    document.querySelectorAll("[tabindex],input,button")
  );

  return focusableElements.map((x) => {
    return {
      location: x.getBoundingClientRect(),
      element: x,
    };
  });
};

const isShiftNavigationShouldBeSkipped = (element, className) => {
  return element.classList.contains(className);
};

const hasPreferMeClass = (element) => {
  return element.classList.contains(preferMeNavigationClass);
};

const focusNext = (
  elementTargetCoordinateSelector,
  leftNavCoordinateSelector,
  rightNavCoordinateSelector,
  isReverse,
  skipShiftNavigationClass
) => {
  let activeElement = document.activeElement;
  let rect = activeElement.getBoundingClientRect();
  let activeElementTargetCoordinate = elementTargetCoordinateSelector(rect);
  let activeElementLeftNavCoordinate = leftNavCoordinateSelector(rect);
  let activeElementRightNavCoordinate = rightNavCoordinateSelector(rect);

  let elementsWithLocation = getFocusableElements();
  let nextElements = [];

  if (isReverse) {
    nextElements = elementsWithLocation.filter(
      (x) =>
        elementTargetCoordinateSelector(x.location) <
          activeElementTargetCoordinate && x.element !== activeElement
    );
  } else {
    nextElements = elementsWithLocation.filter(
      (x) =>
        elementTargetCoordinateSelector(x.location) >
          activeElementTargetCoordinate && x.element !== activeElement
    );
  }

  if (nextElements.length) {
    if (isReverse) {
      nextElements.sort(
        (x, y) =>
          elementTargetCoordinateSelector(y.location) -
          elementTargetCoordinateSelector(x.location)
      );
    } else {
      nextElements.sort(
        (x, y) =>
          elementTargetCoordinateSelector(x.location) -
          elementTargetCoordinateSelector(y.location)
      );
    }

    if (
      isShiftNavigationShouldBeSkipped(activeElement, skipShiftNavigationClass)
    ) {
      nextElements = nextElements.filter(
        (x) =>
          (leftNavCoordinateSelector(x.location) >=
            activeElementLeftNavCoordinate &&
            rightNavCoordinateSelector(x.location) <=
              activeElementRightNavCoordinate) ||
          (leftNavCoordinateSelector(x.location) <=
            activeElementLeftNavCoordinate &&
            rightNavCoordinateSelector(x.location) >=
              activeElementRightNavCoordinate)
      );
    }

    if (nextElements.length) {
      let firstCandidateLocation = elementTargetCoordinateSelector(
        nextElements[0].location
      );
      nextElements = nextElements.filter(
        (x) =>
          Math.abs(
            elementTargetCoordinateSelector(x.location) - firstCandidateLocation
          ) < 15
      );

      if (nextElements.length) {
        nextElements.sort(
          (x, y) =>
            Math.abs(
              leftNavCoordinateSelector(x.location) -
                activeElementLeftNavCoordinate
            ) +
            Math.abs(
              rightNavCoordinateSelector(x.location) -
                activeElementRightNavCoordinate
            ) -
            Math.abs(
              leftNavCoordinateSelector(y.location) -
                activeElementLeftNavCoordinate
            ) -
            Math.abs(
              rightNavCoordinateSelector(y.location) -
                activeElementRightNavCoordinate
            )
        );

        let preferredElement = nextElements.find((x) =>
          hasPreferMeClass(x.element)
        );

        if (preferredElement) {
          preferredElement.element.focus();
        } else {
          nextElements[0].element.focus();
        }
      }
    }
  }
};

const focusTop = () => {
  focusNext(
    (x) => x.top,
    (x) => x.left,
    (x) => x.right,
    true,
    skipShiftTopNavigationClass
  );
};

const focusDown = () => {
  focusNext(
    (x) => x.bottom,
    (x) => x.right,
    (x) => x.left,
    false,
    skipShiftBottomNavigationClass
  );
};

const focusLeft = () => {
  focusNext(
    (x) => x.left,
    (x) => x.bottom,
    (x) => x.top,
    true,
    skipShiftLeftNavigationClass
  );
};

const focusRight = () => {
  focusNext(
    (x) => x.right,
    (x) => x.top,
    (x) => x.bottom,
    false,
    skipShiftRightNavigationClass
  );
};

export function initializeKeyboardNavigation(history) {
  console.log(history);
  document.addEventListener("keydown", function (x) {
    if (x.key === "ArrowUp" || x.keyCode === 38) {
      focusTop();
    } else if (x.key === "ArrowDown" || x.keyCode === 40) {
      focusDown();
    } else if (x.key === "ArrowLeft" || x.keyCode === 37) {
      focusLeft();
    } else if (x.key === "ArrowRight" || x.keyCode === 39) {
      focusRight();
    } else if (x.key === "Backspace" || x.keyCode === 10009) {
      //back
      history.goBack();
    } else if (x.keyCode === 13) {
      //enter
      x.target.click();
      return false;
    }

    // return true;
  });
}
