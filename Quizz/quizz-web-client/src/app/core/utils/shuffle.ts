// Fisher-Yates shuffle
export function shuffle<T>(array: T[]): T[] {
  // Copy for the algorithm so that the original can remain unchanged
  const arrayCopy = [...array];
  let remainingItems = arrayCopy.length;
  let randomIndex: number;

  while (remainingItems > 0) {
    // Pick a remaining element
    randomIndex = Math.floor(Math.random() * remainingItems);
    remainingItems--;
    // And swap it with the current element
    [arrayCopy[remainingItems], arrayCopy[randomIndex]] = [
      arrayCopy[randomIndex],
      arrayCopy[remainingItems],
    ];
  }

  return arrayCopy;
}
