import { TableBody, TableCell, TableRow } from '@mui/material';
import { TableHeader } from '../../../shared/interfaces/Interfaces';
import { useState } from 'react';

interface ContentTableBodyProps {
  headers: TableHeader[];
  items: any[];
}

const DashboardTableBody = ({ headers, items }: ContentTableBodyProps) => {
  const [hoveredItemId, setHoveredItemId] = useState<number | null>(null);

  return (
    <TableBody>
      {items.map((item, index: number) => (
        <TableRow
          key={item.id}
          onMouseEnter={() => setHoveredItemId(item.id)}
          onMouseLeave={() => setHoveredItemId(null)}
          sx={{
            '& > *': { border: 'none' },
            '&:hover': {
              backgroundColor: 'background.paper',
            },
          }}
        >
          {headers.map((header: TableHeader, idx: number) => (
            <TableCell
              key={idx}
              align={header.centered ? 'center' : 'inherit'}
              sx={{ width: header.width, border: 'none' }}
            >
              {header.renderCell
                ? header.renderCell(item, index, hoveredItemId === item.id)
                : item[header.propertyName]}
            </TableCell>
          ))}
        </TableRow>
      ))}
    </TableBody>
  );
};

export default DashboardTableBody;
