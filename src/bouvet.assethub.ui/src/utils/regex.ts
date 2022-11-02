// Capitalize first letter and split the string on capitalized letters
export const capitalizeAndSplit = (key: string) => {
    return key.split(/(?=[A-Z])/).map((p) => { return p[0].toUpperCase() + p.slice(1); }).join(' ');
}