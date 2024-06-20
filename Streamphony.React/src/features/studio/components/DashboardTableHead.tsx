import { TableCell, TableHead, TableRow } from '@mui/material';
import { TableHeader } from '../../../shared/Interfaces';

interface ContentTableHeadProps {
  headers: TableHeader[];
}

const DashboardTableHead = ({ headers }: ContentTableHeadProps) => {
  return (
    <TableHead>
      <TableRow>
        {headers.map((header: TableHeader, index: number) => (
          <TableCell
            key={index}
            align={header.centered ? 'center' : 'inherit'}
            sx={{ width: header.width }}
          >
            {header.icon ? header.icon : header.label}
          </TableCell>
        ))}
      </TableRow>
    </TableHead>
  );
};

export default DashboardTableHead;
