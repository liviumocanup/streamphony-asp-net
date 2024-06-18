import React, { ReactElement, useState } from 'react';
import {
  Table,
  TableBody,
  TableCell,
  TableContainer,
  TableHead,
  TableRow,
} from '@mui/material';

interface Column {
  label: string;
  propertyName: string;
  centered?: boolean;
  width?: string;
  icon?: ReactElement;
  function?: (item: any, index: number, isHovered: boolean) => React.ReactNode;
}

interface ContentTableProps {
  headers: Column[];
  items: any[];
}

const ContentTable = ({ headers, items }: ContentTableProps) => {
  const [hoveredItemId, setHoveredItemId] = useState<number | null>(null);

  return (
    <TableContainer sx={{ boxShadow: 'none' }}>
      <Table sx={{ minWidth: 650 }} aria-label="simple table">
        <TableHead>
          <TableRow>
            {headers.map((header, index) => (
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

        <TableBody>
          {items.map((item, index) => (
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
              {headers.map((header, idx) => (
                <TableCell
                  key={idx}
                  align={header.centered ? 'center' : 'inherit'}
                  sx={{ width: header.width, border: 'none' }}
                >
                  {header.function
                    ? header.function(item, index, hoveredItemId === item.id)
                    : item[header.propertyName]}
                </TableCell>
              ))}
            </TableRow>
          ))}
        </TableBody>
      </Table>
    </TableContainer>
  );
};

export default ContentTable;
