import Html5QrcodePlugin from "./Html5QrcodePlugin.jsx";
import {
	Html5QrcodeResult,
	QrcodeSuccessCallback
} from "html5-qrcode/esm/core";

export default function Html5TsWrapper(prop: { handleDecodedText: (decodedText: string) => void }) {
	const { handleDecodedText } = prop

	const onNewScanResult: QrcodeSuccessCallback = (
		decodedText: string,
		decodedResult: Html5QrcodeResult
	) => {
		handleDecodedText(decodedText)
	};
	return (
		<>
			<Html5QrcodePlugin
				fps={10}
				qrbox={250}
				disableFlip={false}
				qrCodeSuccessCallback={onNewScanResult}
			/>
		</>
	);
}
