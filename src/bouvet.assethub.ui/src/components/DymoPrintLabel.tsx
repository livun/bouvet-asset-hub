import { useEffect } from "react";

export const appendScript = (scriptToAppend : string) => {

    const script = document.createElement("script");
    script.src = scriptToAppend;
    script.async = true;
    document.body.appendChild(script);
}

export default function DymoPrintLabel() {

    useEffect(() => {
        appendScript("../scripts/dymo1.js")
        appendScript("../scripts/dymo2.js")
    })


    return <></>
}