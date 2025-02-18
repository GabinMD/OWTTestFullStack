interface ConfirmModalProps {
  message: string;
  onConfirm: () => void;
  onCancel: () => void;
}

const ConfirmModal = ({ message, onConfirm, onCancel }: ConfirmModalProps) => {
  return (
    <div className="fixed inset-0 bg-black bg-opacity-50 flex items-center justify-center">
      <div className="bg-white p-6 rounded-lg shadow-lg">
        <p className="text-lg font-semibold mb-4 text-black">{message}</p>
        <div className="flex justify-end gap-2">
          <button
            className="px-3 py-1 bg-gray-300 rounded-md"
            onClick={onCancel}
          >
            Annuler
          </button>
          <button
            className="px-3 py-1 bg-red-500 text-white rounded-md hover:bg-red-600 transition"
            onClick={onConfirm}
          >
            Confirmer
          </button>
        </div>
      </div>
    </div>
  );
};

export default ConfirmModal;
