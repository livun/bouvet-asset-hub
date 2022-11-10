export const capitalizeAndSplit = (key: string) => {
    return key.split(/(?=[A-Z])/).map((p) => { return p[0].toUpperCase() + p.slice(1); }).join(' ');
}

export const removeFirstCharacter = (key: string) => {
    return key.substring(1)
}
