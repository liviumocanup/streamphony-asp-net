import { Table, TableContainer } from '@mui/material';
import DashboardTableHead from './DashboardTableHead';
import { TableHeader } from '../../../shared/interfaces/Interfaces';
import DashboardTableBody from './DashboardTableBody';

interface ContentTableProps {
  headers: TableHeader[];
  items: any[];
}

const DashboardTable = ({ headers, items }: ContentTableProps) => {
  return (
    <TableContainer sx={{ boxShadow: 'none' }}>
      <Table sx={{ minWidth: 650 }} aria-label="simple table">
        <DashboardTableHead headers={headers} />

        <DashboardTableBody headers={headers} items={items} />
      </Table>
    </TableContainer>
  );
};

export default DashboardTable;
