import { create } from "zustand";
import axios, { API_APP_URL } from "@/app/_lib/axios";

interface AttachmentState {
  downloadBlobFile: (fileContents: any, type: any, fileName: string) => void;
}

const b64toBlob = (b64Data: any, contentType = "", sliceSize = 512) => {
  const byteCharacters = atob(b64Data);
  const byteArrays = [];

  for (let offset = 0; offset < byteCharacters.length; offset += sliceSize) {
    const slice = byteCharacters.slice(offset, offset + sliceSize);

    const byteNumbers = new Array(slice.length);
    for (let i = 0; i < slice.length; i++) {
      byteNumbers[i] = slice.charCodeAt(i);
    }

    const byteArray = new Uint8Array(byteNumbers);
    byteArrays.push(byteArray);
  }

  const blob = new Blob(byteArrays, { type: contentType });
  return blob;
};

export const useAttachmentStore = create<AttachmentState>((set, get) => ({
  downloadBlobFile: (fileContents: any, type: any, fileName: string) => {
    const blob = b64toBlob(fileContents, type);
    const link = document.createElement("a");
    const url = window.URL.createObjectURL(blob);
    link.href = url;
    link.download = fileName;
    document.body.appendChild(link);
    link.click();
    window.URL.revokeObjectURL(url);
    link?.parentNode?.removeChild(link);
  },
}));
