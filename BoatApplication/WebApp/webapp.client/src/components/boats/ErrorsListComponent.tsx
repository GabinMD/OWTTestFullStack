interface ErrorListProps {
  errors: string[];
}

const ErrorListComponent = ({ errors }: ErrorListProps) => {
  return (
    <div>
      {errors &&
        Object.values(errors).map((error, index) => {
          return (
            <p key={index} className="text-red-500 text-sm text-center mt-2">
              {error}
            </p>
          );
        })}
    </div>
  );
};

export default ErrorListComponent;
