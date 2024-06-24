import { Typography } from '@mui/material';

interface FormSectionProps {
  sectionName: string;
  description: string;
}

const FormSection = ({ sectionName, description }: FormSectionProps) => {
  return (
    <>
      <Typography fontWeight={'bold'}>{sectionName}</Typography>
      <Typography variant="subtitle2" sx={{ mb: 2, color: 'text.disabled' }}>
        {description}
      </Typography>
    </>
  );
};

export default FormSection;
