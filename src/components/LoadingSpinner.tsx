"use client";
import { ClipLoader } from "react-spinners";

export function LoadingSpinner({
  text = "Wczytywanie danych...",
  progress,
}: {
  text?: string;
  progress?: number;
}) {
  return (
    <div className="flex flex-col items-center justify-center h-[70vh] text-gray-600">
      <ClipLoader color="#3b82f6" size={60} />
      <p className="mt-4 text-lg font-medium">
        {text}
        {typeof progress === "number" ? ` (${progress.toFixed(0)}%)` : ""}
      </p>

      {typeof progress === "number" && (
        <div className="w-2/3 h-3 mt-4 bg-gray-200 rounded-full overflow-hidden shadow-inner">
          <div
            className="h-full bg-blue-500 transition-all duration-300"
            style={{ width: `${progress}%` }}
          ></div>
        </div>
      )}
    </div>
  );
}
